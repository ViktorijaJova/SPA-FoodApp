import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment'
import { Observable } from 'rxjs';

@Injectable()
export class AliveService {

    constructor(private http: HttpClient) {}

    serverUrl = environment.apiServer

    ping() : Observable<any> {
        let url = `${this.serverUrl}/api/alive/isalive`;
        return this.http.get(url)
    }

}