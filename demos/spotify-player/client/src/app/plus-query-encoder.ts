import { QueryEncoder } from '@angular/http';

export class PlusQueryEncoder extends QueryEncoder {
    encodeKey(k: string): string {
        k = super.encodeKey(k);
        return k.replace(/\+/gi, '%2B');
    }
    encodeValue(v: string): string {
        v = super.encodeKey(v);
        return v.replace(/\+/gi, '%2B');
    }
}