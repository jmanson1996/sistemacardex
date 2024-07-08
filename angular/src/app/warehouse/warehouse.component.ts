import { Component, Injector } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import {
  PagedListingComponentBase,
  PagedRequestDto
} from '@shared/paged-listing-component-base';
import {
  WarehouseFullDto,
  WarehouseServiceProxy,
  WarehouseViewDto,
  WarehouseViewDtoPagedResultDto
} from '@shared/service-proxies/service-proxies';
import { EditWarehouseComponent } from './edit-warehouse/edit-warehouse.component';
import { CreateWarehouseComponent } from './create-warehouse/create-warehouse.component';


class PagedWarehousesRequestDto extends PagedRequestDto {
  keyword: string;
}

@Component({
  selector: 'app-warehouse',
  templateUrl: './warehouse.component.html',
  styleUrls: ['./warehouse.component.css'],
  animations: [appModuleAnimation()]
})
export class WarehouseComponent extends PagedListingComponentBase<WarehouseViewDto>{
  Warehouses: WarehouseViewDto[] = [];
  keyword = '';

  constructor(
    injector: Injector,
    private _WarehousesService: WarehouseServiceProxy,
    private _modalService: BsModalService
  ) {
    super(injector);
  }

  list(
    request: PagedWarehousesRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    request.keyword = this.keyword;

    this._WarehousesService
      .getAll(request.keyword, request.skipCount, request.maxResultCount)
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result: WarehouseViewDtoPagedResultDto) => {
        this.Warehouses = result.items;
        this.showPaging(result, pageNumber);
      });
  }

  delete(Warehouse: WarehouseViewDto): void {
    abp.message.confirm(
      this.l('DeleteWarningMessage', Warehouse.name),
      undefined,
      (result: boolean) => {
        if (result) {
          this._WarehousesService
            .delete(Warehouse.id)
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

  createWarehouse(): void {
    this.showCreateOrEditWarehouseDialog();
  }

  editWarehouse(Warehouse: WarehouseFullDto): void {
    this.showCreateOrEditWarehouseDialog(Warehouse.id);
  }

  showCreateOrEditWarehouseDialog(id?: any): void {
    let createOrEditWarehouseDialog: BsModalRef;
    if (!id) {
      createOrEditWarehouseDialog = this._modalService.show(
        CreateWarehouseComponent,
        {
          class: 'modal-lg',
        }
      );
    } else {
      createOrEditWarehouseDialog = this._modalService.show(
        EditWarehouseComponent,
        {
          class: 'modal-lg',
          initialState: {
            id: id,
          },
        }
      );
    }

    createOrEditWarehouseDialog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }
}
