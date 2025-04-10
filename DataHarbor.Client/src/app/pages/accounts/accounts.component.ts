import { Component, OnInit } from '@angular/core';
import { Account } from '../../../model/account';
import { AccountsService } from '../../../api/accounts.service';
import { NotificationService } from '../services/notification.service';
import { lastValueFrom } from 'rxjs';
import { RowInsertedEvent } from 'devextreme/ui/data_grid';
import { Guid } from 'devextreme/common';

@Component({
  selector: 'app-accounts',
  templateUrl: './accounts.component.html',
  styleUrl: './accounts.component.css',
  standalone: false
})
export class AccountsComponent implements OnInit {
  protected accounts: Array<Account> = [];
  constructor(private service: AccountsService, private notification: NotificationService) {
  }

  async ngOnInit() {
    this.accounts = await lastValueFrom(this.service.apiAccountsAllGet());
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
}
