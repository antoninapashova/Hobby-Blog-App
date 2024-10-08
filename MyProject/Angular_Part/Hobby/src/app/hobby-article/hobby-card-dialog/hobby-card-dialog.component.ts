import { IHobby } from 'src/app/shared/interfaces/hobby-article';
import { CommentService } from './../services/comment.service';
import { IComment } from './../../shared/interfaces/comment';
import { IPhoto } from './../../shared/interfaces/photo';
import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog } from '@angular/material/dialog';
import { DialogTemplateComponent, ModalType } from 'src/app/core/dialog/dialog-template/dialog-template.component';
import { UserStoreService } from 'src/app/user/user-store.service';
import { UserService } from 'src/app/user/user.service';
import { HobbyService } from '../services/hobby-aticle.service';

@Component({
  selector: 'app-hobby-card-dialog',
  templateUrl: './hobby-card-dialog.component.html',
  styleUrls: ['./hobby-card-dialog.component.css']
})
export class HobbyCardDialogComponent {
  modalTitle: string;
  modalDescription: string;
  modalUsername: string;
  modalLikes: number;
  modalPhotos: IPhoto[];
  modalComments!: IComment[];
  modalId: number | undefined;
  currentUsername!: string | undefined;

  constructor(@Inject(MAT_DIALOG_DATA) public data: IHobby, private commentService: CommentService,
    private matDialog: MatDialog, private userStore: UserStoreService, private userService: UserService, private hobbyService: HobbyService) {
    this.modalTitle = data.title;
    this.modalDescription = data.description;
    this.modalUsername = data.username;
    this.modalPhotos = data.hobbyPhoto;
    this.modalId = data.id;
    this.modalLikes = data.likes;
    this.getComments(data.id);
    this.getFullName();
  }

  getFullName() {
    this.userStore.getFullNameFromStore().subscribe((val: any) => {
      const fullNameFromToken = this.userService.getFullNameFromToken();
      this.currentUsername = val || fullNameFromToken;
    });
  }

  getComments(id: any) {
    this.commentService.getCommentsByHobbyId(id).subscribe({
      next: (res) => {
        this.modalComments = res;
      },
      error: (err) => {
        let obj = { title: 'Comment', message: err.detail, type: ModalType.WARN }
        this.matDialog.open(DialogTemplateComponent, { data: obj });
      }
    });
  }

  likeArticle(){
    let updatedHobby = {...this.data, likes: this.data.likes++}
    this.hobbyService.updateHobby(Number(this.modalId), updatedHobby).subscribe({
      next: () => {},
      error: (err) => {
        let obj = { title: 'Hobby', message: err, type: ModalType.WARN }
        this.matDialog.open(DialogTemplateComponent, { data: obj });
      }
    });
  }
}