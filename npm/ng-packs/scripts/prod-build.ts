import execa from 'execa';
import fse from 'fs-extra';

(async () => {
  await execa('yarn', ['install', '--ignore-scripts'], {
    stdout: 'inherit',
    cwd: '../../../templates/app/angular',
  });

  await fse.remove('../../../templates/app/angular/node_modules/@rocket');

  await fse.copy('../node_modules/@rocket', '../../../templates/app/angular/node_modules/@rocket', {
    overwrite: true,
  });

  await execa('yarn', ['ng', 'build', '--prod'], {
    stdout: 'inherit',
    cwd: '../../../templates/app/angular',
  });
})();
