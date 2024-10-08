import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AddHobbyComponent } from './add-hobby.component';

describe('AddHobbyComponent', () => {
  let component: AddHobbyComponent;
  let fixture: ComponentFixture<AddHobbyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddHobbyComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddHobbyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
