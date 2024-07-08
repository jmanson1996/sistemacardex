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
import { FlatPermissionDto, PermissionDto, UserDto, UserDtoPagedResultDto, UserServiceProxy, ProductFullDto, ProductServiceProxy, ProductViewDto } from '@shared/service-proxies/service-proxies';

@Component({
  selector: 'app-edit-product',
  templateUrl: './edit-product.component.html',
  styleUrls: ['./edit-product.component.css']
})
export class EditProductComponent extends AppComponentBase implements OnInit {
  saving = false;
  id: any;
  Product = new ProductViewDto();
  permissions: FlatPermissionDto[];
  grantedPermissionNames: string[];
  checkedPermissionsMap: { [key: string]: boolean } = {};
  users: UserDto[] = [];

  @Output() onSave = new EventEmitter<any>();

  constructor(
    injector: Injector,
    private _ProductService: ProductServiceProxy,
    private _UserService : UserServiceProxy,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this._ProductService
      .get(this.id)
      .subscribe((result: ProductViewDto) => {
        this.Product = result;
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

    const Product = new ProductViewDto();
    Product.init(this.Product);

    this._ProductService.update(Product).subscribe(
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
