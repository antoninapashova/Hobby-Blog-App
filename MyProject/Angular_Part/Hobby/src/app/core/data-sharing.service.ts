import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable()
export class DataSharingService {
    public isUserLoggedIn: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
    public isCategoryAdded: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
    public isTagAdded: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
    public loggedInUser!: any;

    storeUserId(userId: string) {
        localStorage.setItem('userId', userId);
    }

    getUserId() {
        return localStorage.getItem('userId');
    }
}