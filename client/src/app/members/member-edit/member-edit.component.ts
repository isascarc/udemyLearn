import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { Member } from 'src/app/_mudules/member';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';
import { User } from 'src/_models/user';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {
  @ViewChild('editForm') editForm:NgForm | undefined;
  @HostListener('window:beforeload',['$event']) unloadNotification($event:any){
  if(this.editForm?.dirty){
    $event.returnValue = true;
  }
}

  member: Member | undefined;
  user: User | null = null;

  constructor(private accountService: AccountService,
     private memberService: MembersService, private toaster:ToastrService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => this.user = user

    })
  }

  ngOnInit(): void {
    this.loadMember();
  }

  loadMember(){
    if(!this.user)
      return;
    this.memberService.getMember(this.user.username).subscribe({
      next: member=> {this.member = member;; console.log(this.member)}
    })
  }

  updateMember(){
    this.memberService.updateMember(this.editForm?.value).subscribe({
      next:_=> {this.toaster.success('הפרופיל עודכן בהצלחה');
      this.editForm?.reset(this.member);}
    })
    
  }
}
