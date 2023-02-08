import { IComment } from './../../shared/interfaces/comment';
import { Component, Input} from '@angular/core';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css']
})
export class CommentComponent {

  @Input() comment!: IComment | undefined;
  constructor() { }
}
