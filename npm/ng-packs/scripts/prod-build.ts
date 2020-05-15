import execa from 'execa';
import fse from 'fs-extra';

(async () => {
  await execa('yarn', ['ng', 'build', '--prod'], {
    stdout: 'inherit',
    cwd: '..',
  });

  await execa('yarn', ['install', '--ignore-scripts'], {
    stdout: 'inherit',
    cwd: '../../../templates/app/angular',
  });

  await fse.remove('../../../templates/app/angular/node_modules/@aiwins');

  await fse.copy('../node_modules/@aiwins', '../../../templates/app/angular/node_modules/@aiwins', {
    overwrite: true,
  });

  await execa('yarn', ['ng', 'build', '--prod'], {
    stdout: 'inherit',
    cwd: '../../../templates/app/angular',
  });
})();
