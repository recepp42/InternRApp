import { Directive, Input, TemplateRef } from '@angular/core';
import { ResourceItemPagingResponse } from './entities/resourceItemPagingResponse';
import {  Observable } from 'rxjs';
import {ItemsTmplContext} from '../app/items-tmpl-context'
@Directive({
  selector: '[internRappItemsTmpl]',
  standalone:true
})
export class ItemsTmplDirective<T> {

  public constructor(public readonly templateRef: TemplateRef<T>) {}
  public static ngTemplateContextGuard<T>( _dir: ItemsTmplDirective<T>, ctx: unknown): ctx is ItemsTmplContext<T> 
 {
   return true; 
 }


}
