// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `angular-cli.json`.

export function dataSourceByTenant(): string {
    const tenant = new URLSearchParams(window.location.search).get('tenant');
    if (!tenant || tenant == 'tenant1') {
        return 'http://localhost:5001/odata/Sample';
    } else if (tenant == 'tenant2') {
        return 'http://localhost:5002/odata/Sample';
    }
}

export const environment = {
    sample: dataSourceByTenant(),
    production: false,
};