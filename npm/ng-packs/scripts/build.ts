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
        '@aiwins/ng.core,@aiwins/ng.theme.shared',
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
        '@aiwins/ng.core,@aiwins/ng.theme.shared,@aiwins/ng.feature-management,@aiwins/ng.permission-management,@aiwins/ng.account.config,@aiwins/ng.identity.config,@aiwins/ng.setting-management.config,@aiwins/ng.tenant-management.config',
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
        '@aiwins/ng.feature-management,@aiwins/ng.permission-management,@aiwins/ng.account.config,@aiwins/ng.identity.config,@aiwins/ng.setting-management.config,@aiwins/ng.tenant-management.config',
      ],
      { stdout: 'inherit', cwd: '../' },
    );
  } catch (error) {
    console.error(error.stderr);
    process.exit(1);
  }

  process.exit(0);
})();
