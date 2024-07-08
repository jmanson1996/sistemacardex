import {
  Component,
  Injector,
  OnInit,
  EventEmitter,
  Output,
} from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { forEach as _forEach, includes as _includes, map as _map } from 'lodash-es';
import { AppComponentBase } from '@shared/app-component-base';
import { FlatPermissionDto, PermissionDto, UserDto, UserDtoPagedResultDto, UserServiceProxy, WarehouseFullDto, WarehouseServiceProxy, WarehouseViewDto } from '@shared/service-proxies/service-proxies';

export interface warehouseType {
  id: number;
  name: string;
}

@Component({
  selector: 'app-edit-warehouse',
  templateUrl: './edit-warehouse.component.html',
  styleUrls: ['./edit-warehouse.component.css']
})
export class EditWarehouseComponent extends AppComponentBase implements OnInit {
  saving = false;
  id: any;
  Warehouse = new WarehouseViewDto();
  permissions: FlatPermissionDto[];
  grantedPermissionNames: string[];
  checkedPermissionsMap: { [key: string]: boolean } = {};
  users: UserDto[] = [];

  warehouseType : warehouseType[] = []

  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    private _WarehouseService: WarehouseServiceProxy,
    private _UserService : UserServiceProxy,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }

  ngOnInit(): void {

    this.warehouseType = [
      { id : 1, name : this.l('main') },
      { id : 2, name : this.l('branch') },
    ]

    this._WarehouseService
      .get(this.id)
      .subscribe((result: WarehouseViewDto) => {
        this.Warehouse = result;
        this.setInitialPermissionsStatus();
      });

      this._UserService
      .getAll(
        undefined,
        undefined,
        1,
        100
      )
      .subscribe((result: UserDtoPagedResultDto) => {
        this.users = result.items;
      });
  }

  setInitialPermissionsStatus(): void {
    _map(this.permissions, (item) => {
      this.checkedPermissionsMap[item.name] = this.isPermissionChecked(
        item.name
      );
    });
  }

  isPermissionChecked(permissionName: string): boolean {
    return _includes(this.grantedPermissionNames, permissionName);
  }

  onPermissionChange(permission: PermissionDto, $event) {
    this.checkedPermissionsMap[permission.name] = $event.target.checked;
  }

  getCheckedPermissions(): string[] {
    const permissions: string[] = [];
    _forEach(this.checkedPermissionsMap, function (value, key) {
      if (value) {
        permissions.push(key);
      }
    });
    return permissions;
  }

  save(): void {
    this.saving = true;

    const Warehouse = new WarehouseViewDto();
    Warehouse.init(this.Warehouse);

    console.log(Warehouse);

    this._WarehouseService.update(Warehouse).subscribe(
      () => {
        this.notify.info(this.l('SavedSuccessfully'));
        this.bsModalRef.hide();
        this.onSave.emit();
      },
      () => {
        this.saving = false;
      }
    );
  }
}
