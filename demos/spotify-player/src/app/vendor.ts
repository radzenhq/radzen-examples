// Polyfills
import 'es6-shim';
import 'es6-promise';
import 'zone.js/dist/zone';
import 'reflect-metadata';

if ('production' === process.env.ENV) {
  // Production
} else {
  // Development

  require('zone.js/dist/long-stack-trace-zone');
}

// Angular 2
import '@angular/platform-browser';
import '@angular/platform-browser-dynamic';
import '@angular/core';
import '@angular/common';
import '@angular/http';
import '@angular/router';

// RxJS
import 'rxjs';
