import { Component, Injector, Input } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import {
  PagedListingComponentBase,
  PagedRequestDto
} from '@shared/paged-listing-component-base';
import {
  RoleServiceProxy,
  RoleDto,
  RoleDtoPagedResultDto,
  ProductViewDtoPagedResultDto
} from '@shared/service-proxies/service-proxies';
import { ProductServiceProxy, ProductViewDto } from '@shared/service-proxies/service-proxies';
import { CreateProductComponent } from './create-product/create-product.component';
import { EditProductComponent } from './edit-product/edit-product.component';
class PagedProductRequestDto extends PagedRequestDto {
  keyword: string;
}

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css'],
  animations: [appModuleAnimation()]
})
export class ProductsComponent extends PagedListingComponentBase<ProductViewDto> {
  Product: ProductViewDto[] = [];
  keyword = '';
  @Input() warehouseId: any;

  constructor(
    injector: Injector,
    private _ProductService: ProductServiceProxy,
    private _modalService: BsModalService
  ) {
    super(injector);
  }

  list(
    request: PagedProductRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    request.keyword = this.keyword;

    this._ProductService
      .getAll(request.keyword ,request.skipCount, request.maxResultCount)
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result: ProductViewDtoPagedResultDto) => {
        this.Product = result.items;
        this.showPaging(result, pageNumber);
      });
  }

  delete(Product: ProductViewDto): void {
    abp.message.confirm(
      this.l('DeleteWarningMessage', Product.name),
      undefined,
      (result: boolean) => {
        if (result) {
          this._ProductService
            .delete(Product.id)
            .pipe(
              finalize(() => {
                abp.notify.success(this.l('SuccessfullyDeleted'));
                this.refresh();
              })
            )
            .subscribe(() => {});
        }
      }
    );
  }

  createProduct(): void {
    this.showCreateOrEditProductDialog();
  }

  editProduct(Product: ProductViewDto): void {
    this.showCreateOrEditProductDialog(Product.id);
  }

  showCreateOrEditProductDialog(id?: number): void {
    let createOrEditProductDialog: BsModalRef;
    if (!id) {
      createOrEditProductDialog = this._modalService.show(
        CreateProductComponent,
        {
          class: 'modal-lg',
          initialState : {
            warehouseId : this.warehouseId
          }
        }
      );
    } else {
      createOrEditProductDialog = this._modalService.show(
        EditProductComponent,
        {
          class: 'modal-lg',
          initialState: {
            id: id,
          },
        }
      );
    }

    createOrEditProductDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }
}
