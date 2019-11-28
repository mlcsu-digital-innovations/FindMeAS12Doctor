import { AddressResult } from 'src/app/interfaces/address-result';
import { environment } from '../../../environments/environment';
import { filter, delay } from 'rxjs/operators';
import { from, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PostcodeValidationService {

  constructor(private httpClient: HttpClient) {}

  public validatePostcode(postcode: string) {
    return this.httpClient.get(
      `${environment.apiEndpoint}/locationdetails/${postcode}`
    );
  }

  public searchPostcode(postcode: string): Observable<AddressResult> {

    // format the supplied postcode so that it contains a space
    postcode = postcode.trim();
    if (postcode.indexOf(' ') === -1 && postcode.length > 3) {
      const inwardCode = postcode.substr(postcode.length - 3, 3);
      const outwardCode = postcode.substr(0, postcode.length - 3);
      postcode = `${outwardCode} ${inwardCode}`;
    }

    // Dummy data for postcode searching !!

    const addresses = from([
      {postcode: 'WS11 5UB', address: '1 Blake Close, Cannock, Staffs, WS11 5UB'},
      {postcode: 'WS11 5UB', address: '1a Blake Close, Cannock, Staffs, WS11 5UB'},
      {postcode: 'WS11 5UB', address: '2 Blake Close, Cannock, Staffs, WS11 5UB'},
      {postcode: 'WS11 5UB', address: '2a Blake Close, Cannock, Staffs, WS11 5UB'},
      {postcode: 'WS11 5UB', address: '3 Blake Close, Cannock, Staffs, WS11 5UB'},
      {postcode: 'WS11 5UB', address: '3a Blake Close, Cannock, Staffs, WS11 5UB'},
      {postcode: 'WS11 5UB', address: '4 Blake Close, Cannock, Staffs, WS11 5UB'},
      {postcode: 'WS11 5UB', address: '5 Blake Close, Cannock, Staffs, WS11 5UB'},
      {postcode: 'WS11 5UB', address: '6 Blake Close, Cannock, Staffs, WS11 5UB'},
      {postcode: 'WS11 5UB', address: '7 Blake Close, Cannock, Staffs, WS11 5UB'},
      {postcode: 'WS11 5UB', address: '8 Blake Close, Cannock, Staffs, WS11 5UB'},
      {postcode: 'WS11 5UB', address: '9 Blake Close, Cannock, Staffs, WS11 5UB'},
      {postcode: 'WS11 5UB', address: '10 Blake Close, Cannock, Staffs, WS11 5UB'},
      {postcode: 'WS11 5UB', address: '11 Blake Close, Cannock, Staffs, WS11 5UB'},
      {postcode: 'WS11 5UB', address: '12 Blake Close, Cannock, Staffs, WS11 5UB'},
      {postcode: 'WS11 5UB', address: '13 Blake Close, Cannock, Staffs, WS11 5UB'},
      {postcode: 'WS11 5UB', address: '14 Blake Close, Cannock, Staffs, WS11 5UB'},
      {postcode: 'WS11 5UB', address: '15 Blake Close, Cannock, Staffs, WS11 5UB'},
      {postcode: 'WS11 5UB', address: '16 Blake Close, Cannock, Staffs, WS11 5UB'},
      {postcode: 'WS11 5UB', address: '17 Blake Close, Cannock, Staffs, WS11 5UB'},
      {postcode: 'WS11 5UB', address: '18 Blake Close, Cannock, Staffs, WS11 5UB'},
      {postcode: 'WS11 5UB', address: '19 Blake Close, Cannock, Staffs, WS11 5UB'},
      {postcode: 'WS11 5UB', address: '20 Blake Close, Cannock, Staffs, WS11 5UB'},
      {postcode: 'WS11 5UB', address: '21 Blake Close, Cannock, Staffs, WS11 5UB'},
      {postcode: 'WS11 5UB', address: '22 Blake Close, Cannock, Staffs, WS11 5UB'},
      {postcode: 'WS11 5UB', address: '23 Blake Close, Cannock, Staffs, WS11 5UB'},
      {postcode: 'WS11 5UB', address: '24 Blake Close, Cannock, Staffs, WS11 5UB'},
      {postcode: 'WS11 5UB', address: '25 Blake Close, Cannock, Staffs, WS11 5UB'},
      {postcode: 'WS11 5UB', address: '26 Blake Close, Cannock, Staffs, WS11 5UB'},
      {postcode: 'WS11 5UB', address: '27 Blake Close, Cannock, Staffs, WS11 5UB'},
      {postcode: 'WS11 5UB', address: '28 Blake Close, Cannock, Staffs, WS11 5UB'},
      {postcode: 'WS11 5UB', address: '29 Blake Close, Cannock, Staffs, WS11 5UB'},
      {postcode: 'WS11 5UB', address: '30 Blake Close, Cannock, Staffs, WS11 5UB'},
      {postcode: 'WS12 4SY', address: '1 White Bark Close, Hednesford, Cannock, Staffs, WS12 4SY'},
      {postcode: 'WS12 4SY', address: '2 White Bark Close, Hednesford, Cannock, Staffs, WS12 4SY'},
      {postcode: 'WS12 4SY', address: '3 White Bark Close, Hednesford, Cannock, Staffs, WS12 4SY'},
      {postcode: 'WS12 4SY', address: '4 White Bark Close, Hednesford, Cannock, Staffs, WS12 4SY'},
      {postcode: 'WS12 4SY', address: '5 White Bark Close, Hednesford, Cannock, Staffs, WS12 4SY'}
    ]);

    return addresses
      .pipe(delay(2000))
      .pipe(filter(address => address.postcode === postcode || address.postcode === ''));
  }
}
