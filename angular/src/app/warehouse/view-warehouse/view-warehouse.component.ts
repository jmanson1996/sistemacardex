import { Component, Injector, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { appModuleAnimation } from '@shared/animations/routerTransition';
import { AppComponentBase } from '@shared/app-component-base';
import { BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-view-warehouse',
  templateUrl: './view-warehouse.component.html',
  styleUrls: ['./view-warehouse.component.css'],
  animations: [appModuleAnimation()]
})
export class ViewWarehouseComponent extends AppComponentBase implements OnInit{
  selectedTab: string = 'inventory';
  warehouseId : string;
  selectTab(tab: string) {
    this.selectedTab = tab;
  }

  constructor(
    injector: Injector,
    private _modalService: BsModalService,
    private route: ActivatedRoute,
    private router: Router
  )
  {
    super(injector);
  }

  async ngOnInit(): Promise<void> {
    this.route.paramMap.subscribe(params => {
      this.warehouseId = params.get('id');
    });

    // if (this.idCourse) {
    //   await this.loadCourse(this.idCourse);
    //   if (this.course && this.course.result && this.course.result.userTeacherId) {
    //     await this.loadUser(this.course.result.userTeacherId.toString());
    //   }
    // }
  }

}
