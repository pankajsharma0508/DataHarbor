import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../shared/services';
import { IUser } from '../../../authentication/keycloak.service';

@Component({
  templateUrl: 'home.component.html',
  styleUrls: ['./home.component.scss'],
  standalone: false
})

export class HomeComponent implements OnInit {
  protected userProfile: IUser | null = null;
  constructor(private authService: AuthService) {
  }
  ngOnInit(): void {
    this.authService.getUser().then(x => this.userProfile = x.data);
  }
}
