import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SignalRChatComponent } from './signal-r-chat.component';

describe('SignalRChatComponent', () => {
  let component: SignalRChatComponent;
  let fixture: ComponentFixture<SignalRChatComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SignalRChatComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SignalRChatComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
