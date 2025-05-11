import { Component } from '@angular/core';

@Component({
  selector: 'app-insights',
  templateUrl: './insights.component.html',
  styleUrl: './insights.component.css',
  standalone: false
})
export class InsightsComponent {
  protected grafanaUrl ='http://grafana.local/d/WZhXkZaHk/data-harbor?orgId=1&kiosk'
}
