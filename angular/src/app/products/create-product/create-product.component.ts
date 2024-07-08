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
import { ProductServiceProxy, UserServiceProxy, UserDtoPagedResultDto, UserDto, ProductFullDto } from '@shared/service-proxies/service-proxies';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-create-product',
  templateUrl: './create-product.component.html',
  styleUrls: ['./create-product.component.css'],
  animations: [appModuleAnimation()]
})
export class CreateProductComponent extends AppComponentBase
implements OnInit {
  saving = false;
  Product = new ProductFullDto();
  checkedPermissionsMap: { [key: string]: boolean } = {};
  defaultPermissionCheckedStatus = true;
  warehouseId : string;

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
    const Product = new ProductFullDto();
    //this.Product.warehouseId = this.warehouseId;
    Product.init(this.Product);

    this._ProductService
      .create(Product)
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
