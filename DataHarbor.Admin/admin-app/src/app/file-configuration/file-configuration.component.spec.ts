import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FileConfigurationComponent } from './file-configuration.component';

describe('FileConfigurationComponent', () => {
  let component: FileConfigurationComponent;
  let fixture: ComponentFixture<FileConfigurationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FileConfigurationComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FileConfigurationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
