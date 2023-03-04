import { DataSharingService } from './../../core/data-sharing.service';
import { UserStoreService } from './../user-store.service';
import { ModalType } from './../../core/dialog/dialog-template/dialog-template.component';
import { MatDialog } from '@angular/material/dialog';
import { UserService } from './../user.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup,  Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { DialogTemplateComponent } from 'src/app/core/dialog/dialog-template/dialog-template.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
   
  invalidLogin?: boolean;
  hide : boolean = true;
  loginUserForm: FormGroup = new FormGroup({});

  constructor(private formBuilder: FormBuilder, private router: Router, 
              private userService: UserService,private jwtHelper : JwtHelperService,
              private matDialog: MatDialog, private userStoreService: UserStoreService, private dataSharingService: DataSharingService
           ) { }

  ngOnInit(): void {
       this.loginUserForm = this.formBuilder.group({
         username: [null, [Validators.required, Validators.minLength(3)]],
         password: [null, [Validators.required, Validators.minLength(5)]],
       });
  }
  
  onSubmit (form: FormGroup) {
     if(form.valid){         
       this.userService.login(form.value).subscribe({
        next:(res)=>{
          let obj ={title: 'Login', message: 'Login is successful', type: ModalType.INFO}
          this.matDialog.open( DialogTemplateComponent, {data: obj});
          console.log(res.accessToken);
          console.log(res.refreshToken);
          this.userService.storeToken(res.accessToken);
          this.userService.storeRefreshToken(res.refreshToken);
          const tokenPayload = this.userService.decodeToken();
          this.userStoreService.setFullName(tokenPayload.unique_name);
          this.userStoreService.setRoleForStore(tokenPayload.role);
          this.router.navigate(['home']);
          this.dataSharingService.isUserLoggedIn.next(true);
         },
        error:(err)=>{
          let obj ={title: 'Login', message: err.error.detail, type: ModalType.WARN}
          console.log(obj);
          this.matDialog.open( DialogTemplateComponent, {data: obj})
        }
       })
     }
   }

  isUserAuthenticated() {
    const token = localStorage.getItem("jwt");
    if (token && !this.jwtHelper.isTokenExpired(token)) {
      return true;
    }
    else {return false;}
  }

}
