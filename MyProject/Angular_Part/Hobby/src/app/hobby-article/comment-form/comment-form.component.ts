import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-comment-form',
  templateUrl: './comment-form.component.html',
  styleUrls: ['./comment-form.component.css']
})
export class CommentFormComponent implements OnInit{
  commentForm: FormGroup = new FormGroup({});
  
  @Input() submitLabel!: string;
  @Input() hasCancelButton: boolean = false;
  @Input() initialText: string = '';
  
  @Output() handleSubmit = new EventEmitter<string>();
  @Output() handleCancel = new EventEmitter<void>();
  
  constructor(private fb: FormBuilder) {}
  
  ngOnInit(): void {
    this.commentForm = this.fb.group({
      commentContent: [this.initialText, Validators.required],
    });
  }

  onSubmit(form: FormGroup): void {
    this.handleSubmit.emit(form.value.commentContent);
    this.commentForm.reset();
  }
}
