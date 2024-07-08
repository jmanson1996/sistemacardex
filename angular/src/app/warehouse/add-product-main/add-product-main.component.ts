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
import { WarehouseServiceProxy, WarehouseFullDto, UserServiceProxy, UserDtoPagedResultDto, UserDto, ProductServiceProxy, ProductViewDto, ProductViewDtoPagedResultDto, AddProductToWarehouseDto, InventaryServiceProxy } from '@shared/service-proxies/service-proxies';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { finalize } from 'rxjs';

@Component({
  selector: 'app-add-product-main',
  templateUrl: './add-product-main.component.html',
  styleUrls: ['./add-product-main.component.css'],
  animations: [appModuleAnimation()]
})
export class AddProductMainComponent extends AppComponentBase
implements OnInit {

  addProductMain = new AddProductToWarehouseDto();
  Product: ProductViewDto[] = [];
  productQuantity: number;
  selectedProduct: string;
  saving = false;

  constructor(
    injector: Injector,
    private _WarehouseService: WarehouseServiceProxy,
    private _ProductService: ProductServiceProxy,
    private _UserService : UserServiceProxy,
    private _InventaryService : InventaryServiceProxy,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this._ProductService
      .getAll(undefined ,0, 1000)
      .subscribe((result: ProductViewDtoPagedResultDto) => {
        this.Product = result.items;
      });
  }

  onProductSelected(product: ProductViewDto) {
    this.selectedProduct = product.name;
    this._InventaryService
      .countProductInventary(product.id)
      .subscribe((result: Number) => {
        console.log(result);
        this.productQuantity = result.valueOf();
      });
  }

 // constructor(private productService: ProductService) { }

  save() {
    console.log(this.addProductMain);
    this.saving = true;
    const Product = new AddProductToWarehouseDto();
    //this.Product.warehouseId = this.warehouseId;
    Product.init(this.addProductMain);

    this._ProductService
      .addProductToMainWarehouse(Product)
      .subscribe(
        () => {
          this.notify.info(this.l('SavedSuccessfully'));
          this.bsModalRef.hide();
          //this.onSave.emit();
        },
        () => {
          this.saving = false;
        }
      );
  }
}
