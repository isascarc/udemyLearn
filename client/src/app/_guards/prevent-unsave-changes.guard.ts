import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanDeactivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { MemberEditComponent } from '../members/member-edit/member-edit.component';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsaveChangesGuard implements CanDeactivate<MemberEditComponent> {
  canDeactivate(component: MemberEditComponent):  boolean  {
    if(component.editForm?.dirty){
      return confirm('יש לך שינויים שלא נשמרו. אתה בטוח שאתה מעוניין לצאת?')
    }
    return true;
  }
  
}
