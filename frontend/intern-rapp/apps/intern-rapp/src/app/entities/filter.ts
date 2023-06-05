import { Observable } from 'rxjs';
import { FilterType } from '../enums/filterType';

export interface Filter {
  type: FilterType;
  name: string;
  label: string;
  observable: Observable<unknown[]> | undefined;
  optionBuilder(items: unknown[], value: unknown): unknown[] | undefined,
  defaultValue:unknown|undefined
}
