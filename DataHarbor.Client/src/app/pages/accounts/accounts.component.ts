import { Component, OnInit } from '@angular/core';
import { Account } from '../../../model/account';
import { AccountsService } from '../../../api/accounts.service';
import { NotificationService } from '../services/notification.service';
import { lastValueFrom } from 'rxjs';

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
}
