import { TranslateService } from "@ngx-translate/core";
import { LanguageOption } from "./enums/languageDropdownOptions";
import { tap, timer } from "rxjs";

export function AppInitializerFactory(translateService: TranslateService)
{
  function capitalize(s: string) {
    return s[0].toUpperCase() + s.slice(1);
    }
    return () => {
        
          const capitalizedLanguage = capitalize(
            translateService.getBrowserCultureLang()?.split('-')[0] as string
        );
          if (Object.keys(LanguageOption).includes(capitalizedLanguage)) {
            translateService.currentLang = capitalizedLanguage;
          } else {
            translateService.currentLang = 'Nl';
          }
      // translateService.setDefaultLang(translateService.currentLang);
      translateService.use(translateService.currentLang);
      
      return translateService.getTranslation(translateService.currentLang);
    }

}
// function capitalize(s: string) {return  s[0].toUpperCase() + s.slice(1); }
