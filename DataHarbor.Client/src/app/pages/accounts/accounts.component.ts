import { Component, OnInit } from '@angular/core';
import { Account } from '../../../model/account';
import { AccountsService } from '../../../api/accounts.service';
import { NotificationService } from '../services/notification.service';
import { lastValueFrom } from 'rxjs';
import { RowInsertedEvent } from 'devextreme/ui/data_grid';
import { Guid } from 'devextreme/common';
import { DxDataGridTypes } from 'devextreme-angular/ui/data-grid';
import { ExportService } from '../../shared/services/export.service';
import { LoadPanelService } from '../../shared/services';

@Component({
  selector: 'app-accounts',
  templateUrl: './accounts.component.html',
  styleUrl: './accounts.component.css',
  standalone: false
})
export class AccountsComponent implements OnInit {
  protected accounts: Array<Account> = [];
  constructor(private service: AccountsService,
    private exportService: ExportService,
    private loadService: LoadPanelService,
    private notification: NotificationService) {
  }

  async ngOnInit() {
    try {
      this.loadService.show();
      this.accounts = await lastValueFrom(this.service.apiAccountsAllGet());
    } catch (ex) {
      this.notification.showError('Error while loading..');
    } finally {
      this.loadService.hide();
    }
  }

  async onRowInserted(event: RowInsertedEvent) {
    try {
      const account = event.data as Account;
      account.uniqueId = new Guid().toString();

      await lastValueFrom(this.service.apiAccountsCreatePost(account));
      this.notification.showSuccess(`Account ${account.name} created successfully.`);
    } catch (e) {
      this.notification.showError(`Unable to create account.`);
    }
  }
  onExporting(e: DxDataGridTypes.ExportingEvent) {
    this.exportService.onExporting('Accounts', e);
  }
}
