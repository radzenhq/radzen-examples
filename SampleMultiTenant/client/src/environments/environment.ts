// The file contents for the current environment will overwrite these during build.
// The build system defaults to the dev environment which uses `environment.ts`, but if you do
// `ng build --env=prod` then `environment.prod.ts` will be used instead.
// The list of which env maps to which file can be found in `angular-cli.json`.
export function dataSourceRoot(): string {
  return 'http://localhost:5001';
}

export const environment = {
  sample: `${dataSourceRoot()}/odata/Sample`,
  securityUrl: `${dataSourceRoot()}/auth`,
  production: false,
};