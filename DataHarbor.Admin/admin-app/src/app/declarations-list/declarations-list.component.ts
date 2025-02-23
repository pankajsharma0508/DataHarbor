import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { ProcessRequest } from '../../model/processRequest';
import { ProcessRequestService } from '../../api/processRequest.service';
import { DxDataGridModule, DxTabPanelModule } from 'devextreme-angular';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-declarations-list',
  imports: [CommonModule, DxDataGridModule, RouterModule],
  templateUrl: './declarations-list.component.html',
  styleUrl: './declarations-list.component.scss',
  standalone: true
})
export class DeclarationsListComponent implements OnInit {

  protected requests: ProcessRequest[] = [];
  constructor(private service: ProcessRequestService) {
  }

  ngOnInit(): void {
    this.loadConfigurations();
  }

  async loadConfigurations() {
    const requests = await lastValueFrom(this.service.apiProcessRequestGet());
    this.requests = requests;
  }

}
