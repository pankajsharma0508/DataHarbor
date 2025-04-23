import { Component, HostBinding } from '@angular/core';
import { AuthService, ScreenService, AppInfoService } from './shared/services';
import { LoadPanelService } from './shared/services/load-panel.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  standalone: false
})
export class AppComponent {
  @HostBinding('class') get getClass() {
    const sizeClassName = Object.keys(this.screen.sizes).filter(cl => this.screen.sizes[cl]).join(' ');
    return `${sizeClassName} app`;
  }

  constructor(private authService: AuthService,
    private screen: ScreenService,
    protected loadService: LoadPanelService,
    public appInfo: AppInfoService) { } 
    
  get isVisible() {
    return this.loadService.isVisible$ || false;
  }

  isAuthenticated() {
    return this.authService.loggedIn;
  }
}
