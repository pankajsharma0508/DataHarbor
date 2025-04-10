import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NotificationService } from '../services/notification.service';
import { AccountsService } from '../../../api/accounts.service';
import { Account } from '../../../model/account';
import { lastValueFrom } from 'rxjs';
import { DxDataGridTypes } from 'devextreme-angular/ui/data-grid';
import { ExportService } from '../../shared/services/export.service';

@Component({
  selector: 'app-account',
  templateUrl: './account.component.html',
  styleUrl: './account.component.css',
  standalone: false
})
export class AccountComponent {
  protected accountId: string | undefined;
  protected account: Account | undefined;

  constructor(
    private accountService: AccountsService,
    private notification: NotificationService,
    private exportService: ExportService,
    private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.accountId = params.get('id') || '';
      this.loadAccount(this.accountId)
    });

  }

  async loadAccount(id: string) {
    try {
      this.account = await lastValueFrom(this.accountService.apiAccountsIdGet(id));
    } catch (ex) {
    }
  }
  get transactions() {
    return this.account?.transactions || [];
  }

  onExporting(e: DxDataGridTypes.ExportingEvent) {
    this.exportService.onExporting(this.account?.name, e);
  }
}
