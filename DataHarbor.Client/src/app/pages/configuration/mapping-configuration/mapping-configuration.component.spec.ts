import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MappingConfigurationComponent } from './mapping-configuration.component';

describe('MappingConfigurationComponent', () => {
  let component: MappingConfigurationComponent;
  let fixture: ComponentFixture<MappingConfigurationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MappingConfigurationComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MappingConfigurationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
