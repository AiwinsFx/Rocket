import execa from 'execa';
import program from 'commander';

(async () => {
  program.option('-i, --noInstall', 'skip updating package.json and installation', false);

  program.parse(process.argv);

  try {
    if (!program.noInstall) {
      await execa('yarn', ['install-new-dependencies'], { stdout: 'inherit' });
    }

    await execa(
      'yarn',
      [
        'symlink',
        'copy',
        '--angular',
        '--no-watch',
        '--sync',
        '--packages',
        '@rocket/ng.core,@rocket/ng.theme.shared',
      ],
      { stdout: 'inherit', cwd: '../' },
    );

    await execa(
      'yarn',
      [
        'symlink',
        'copy',
        '--angular',
        '--no-watch',
        '--packages',
        '@rocket/ng.feature-management,@rocket/ng.permission-management,@rocket/ng.account.config,@rocket/ng.identity.config,@rocket/ng.setting-management.config,@rocket/ng.tenant-management.config',
      ],
      { stdout: 'inherit', cwd: '../' },
    );

    await execa(
      'yarn',
      [
        'symlink',
        'copy',
        '--angular',
        '--no-watch',
        '--all-packages',
        '--excluded-packages',
        '@rocket/ng.core,@rocket/ng.theme.shared,@rocket/ng.feature-management,@rocket/ng.permission-management,@rocket/ng.account.config,@rocket/ng.identity.config,@rocket/ng.setting-management.config,@rocket/ng.tenant-management.config',
      ],
      { stdout: 'inherit', cwd: '../' },
    );
  } catch (error) {
    console.error(error.stderr);
    process.exit(1);
  }

  process.exit(0);
})();
