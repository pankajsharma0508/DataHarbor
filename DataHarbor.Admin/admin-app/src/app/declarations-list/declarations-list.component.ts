import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { lastValueFrom } from 'rxjs';
import { DxDataGridModule } from 'devextreme-angular';
import { RouterModule } from '@angular/router';
import { DeclarationService } from '../../api/declaration.service';
import { Declaration } from '../../model/declaration';

@Component({
  selector: 'app-declarations-list',
  imports: [CommonModule, DxDataGridModule, RouterModule],
  templateUrl: './declarations-list.component.html',
  styleUrl: './declarations-list.component.scss',
  standalone: true
})
export class DeclarationsListComponent implements OnInit {

  protected requests: Declaration[] = [];
  constructor(private service: DeclarationService) {
  }

  ngOnInit(): void {
    this.loadConfigurations();
  }

  async loadConfigurations() {
    const requests = await lastValueFrom(this.service.apiDeclarationGet());
    this.requests = requests;
  }

}
