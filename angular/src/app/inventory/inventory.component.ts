import { Component, Injector } from '@angular/core';
import { finalize } from 'rxjs/operators';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import {
  PagedListingComponentBase,
  PagedRequestDto
} from '@shared/paged-listing-component-base';
import {
  InventaryFullDto,
  InventaryServiceProxy,
  InventaryViewDto,
  InventaryViewDtoPagedResultDto
} from '@shared/service-proxies/service-proxies';


class PagedInventarysRequestDto extends PagedRequestDto {
  keyword: string;
}

@Component({
  selector: 'app-inventory',
  templateUrl: './inventory.component.html',
  styleUrls: ['./inventory.component.css'],
  animations: [appModuleAnimation()]
})
export class InventoryComponent extends PagedListingComponentBase<InventaryViewDto> {
  Inventarys: InventaryViewDto[] = [];
  keyword = '';

  constructor(
    injector: Injector,
    private _InventarysService: InventaryServiceProxy,
    private _modalService: BsModalService
  ) {
    super(injector);
  }

  list(
    request: PagedInventarysRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    request.keyword = this.keyword;

    this._InventarysService
      .getAll(request.keyword,undefined,undefined, request.skipCount, request.maxResultCount)
      .pipe(
        finalize(() => {
          finishedCallback();
        })
      )
      .subscribe((result: InventaryViewDtoPagedResultDto) => {
        this.Inventarys = result.items;
        this.showPaging(result, pageNumber);
      });
  }

  delete(Inventary: InventaryViewDto): void {
    abp.message.confirm(
      this.l('DeleteWarningMessage', Inventary.id),
      undefined,
      (result: boolean) => {
        if (result) {
          this._InventarysService
            .delete(Inventary.id)
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

  createInventary(): void {
    this.showCreateOrEditInventaryDialog();
  }

  editInventary(Inventary: InventaryFullDto): void {
    this.showCreateOrEditInventaryDialog(Inventary.warehouseId);
  }

  showCreateOrEditInventaryDialog(id?: any): void {
    // let createOrEditInventaryDialog: BsModalRef;
    // if (!id) {
    //   createOrEditInventaryDialog = this._modalService.show(
    //     CreateInventaryComponent,
    //     {
    //       class: 'modal-lg',
    //     }
    //   );
    // } else {
    //   createOrEditInventaryDialog = this._modalService.show(
    //     EditInventaryComponent,
    //     {
    //       class: 'modal-lg',
    //       initialState: {
    //         id: id,
    //       },
    //     }
    //   );
    // }

    // createOrEditInventaryDialog.content.onSave.subscribe(() => {
    //   this.refresh();
    // });
  }
}
