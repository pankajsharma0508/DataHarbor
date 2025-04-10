import Keycloak from 'keycloak-js';

export class KeycloakService {
  private keycloak!: Keycloak;

  init(): Promise<boolean> {
    this.keycloak = new Keycloak({
      url: 'http://localhost:8080', // Your Keycloak server
      realm: 'DataHarbor',
      clientId: 'DataHarborClient',
    });

    return this.keycloak.init({
      onLoad: 'login-required',
      checkLoginIframe: false,
      pkceMethod: 'S256',
    }).then(authenticated => {
      return authenticated;
    });
  }

  getKeycloakInstance(): Keycloak {
    return this.keycloak;
  }

  isLoggedIn(): boolean {
    return this.keycloak?.authenticated ?? false;
  }

  getToken(): string | undefined {
    return this.keycloak.token;
  }

  logout(): void {
    this.keycloak.logout();
  }
  
  login(): void {
    this.keycloak.login();
  }

  getUserProfile(): IUser | null {
    if (!this.keycloak.tokenParsed) return null;
    return {
      userName: this.keycloak.tokenParsed?.['preferred_username'],
      email: this.keycloak.tokenParsed?.['email'],
      firstName: this.keycloak.tokenParsed?.['given_name'],
      lastName: this.keycloak.tokenParsed?.['family_name'],
      roles: this.keycloak.tokenParsed?.realm_access?.roles || []
    };
  }
}


export interface IUser {
  firstName: string;
  lastName: string;
  email: string;
  userName: string;
  roles: string[];
}
