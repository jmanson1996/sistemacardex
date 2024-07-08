import {
  Component,
  Injector,
  OnInit,
  EventEmitter,
  Output,
} from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AppComponentBase } from '@shared/app-component-base';
import { forEach as _forEach, map as _map } from 'lodash-es';
import { WarehouseServiceProxy, WarehouseFullDto, UserServiceProxy, UserDtoPagedResultDto, UserDto } from '@shared/service-proxies/service-proxies';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { finalize } from 'rxjs';

export interface warehouseType {
  id: number;
  name: string;
}

@Component({
  selector: 'app-create-warehouse',
  templateUrl: './create-warehouse.component.html',
  styleUrls: ['./create-warehouse.component.css'],
  animations: [appModuleAnimation()]
})

export class CreateWarehouseComponent extends AppComponentBase
implements OnInit {
  saving = false;
  Warehouse = new WarehouseFullDto();
  checkedPermissionsMap: { [key: string]: boolean } = {};
  defaultPermissionCheckedStatus = true;
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
    console.log(this.l('main'));
    this.warehouseType = [
      { id : 1, name : this.l('main') },
      { id : 2, name : this.l('branch') },
    ]
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

  selectedItem: any;

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
    console.log(this.Warehouse);
    const Warehouse = new WarehouseFullDto();
    Warehouse.init(this.Warehouse);

    this._WarehouseService
      .create(Warehouse)
      .subscribe(
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
