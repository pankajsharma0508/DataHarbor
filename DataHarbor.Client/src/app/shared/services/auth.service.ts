import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { IUser, KeycloakService } from '../../../authentication/keycloak.service';

const defaultPath = '/';

@Injectable()
export class AuthService {
  private _user: IUser | null = null;
  get loggedIn(): boolean {
    return !!this.keyClock.isLoggedIn();
  }

  private _lastAuthenticatedPath: string = defaultPath;
  set lastAuthenticatedPath(value: string) {
    this._lastAuthenticatedPath = value;
  }

  constructor(private router: Router, private keyClock: KeycloakService) {
  }

  async logIn(email: string, password: string) {

    try {
      // Send request
      this.router.navigate([this._lastAuthenticatedPath]);
      return {
        isOk: true,
        data: this._user
      };
    }
    catch {
      return {
        isOk: false,
        message: "Authentication failed"
      };
    }
  }

  async getUser() {
    try {
      // Send request
      this._user = this.keyClock.getUserProfile();

      return {
        isOk: true,
        data: this._user
      };
    }
    catch {
      return {
        isOk: false,
        data: null
      };
    }
  }

  async createAccount(email: string, password: string) {
    try {
      // Send request

      this.router.navigate(['/create-account']);
      return {
        isOk: true
      };
    }
    catch {
      return {
        isOk: false,
        message: "Failed to create account"
      };
    }
  }

  async changePassword(email: string, recoveryCode: string) {
    try {
      // Send request

      return {
        isOk: true
      };
    }
    catch {
      return {
        isOk: false,
        message: "Failed to change password"
      }
    }
  }

  async resetPassword(email: string) {
    try {
      // Send request

      return {
        isOk: true
      };
    }
    catch {
      return {
        isOk: false,
        message: "Failed to reset password"
      };
    }
  }

  async logOut() {
    this.keyClock.logout();
    this._user = null;
    this.keyClock.login();
  }
}
