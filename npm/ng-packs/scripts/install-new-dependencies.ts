import execa from 'execa';
import fse from 'fs-extra';

console.warn(1111);

const updateAndInstall = async () => {
  const { projects } = await fse.readJSON('../angular.json');
  const projectNames = Object.keys(projects).filter(project => project !== 'dev-app');
  console.warn(projectNames);
  const packageJson = await fse.readJSON('../package.json');
  console.warn(packageJson);
  projectNames.forEach(project => {
    // do not convert to async
    const { dependencies = {}, peerDependencies = {}, name, version } = fse.readJSONSync(
      `../packages/${project}/package.json`,
    );

    packageJson.devDependencies = {
      ...packageJson.devDependencies,
      ...dependencies,
      ...peerDependencies,
      [name]: `~${version}`,
    };

    console.warn(packageJson.devDependencies);

    packageJson.devDependencies = Object.keys(packageJson.devDependencies)
      .sort()
      .reduce((acc, key) => ({ ...acc, [key]: packageJson.devDependencies[key] }), {});
  });

  console.warn('Searching the packages on NPM to check if it is exist. It takes a while.');
  Object.keys(packageJson.devDependencies).forEach(pkg => {
    const isPackageExistOnNPM = !(
      execa.sync('npm', ['search', pkg]).stdout.indexOf('No matches found for') > -1
    );

    if (!isPackageExistOnNPM) delete packageJson.devDependencies[pkg];
  });

  await fse.writeJSON('../package.json', packageJson, { spaces: 2 });

  try {
    await execa('yarn', ['install', '--ignore-scripts'], {
      stdout: 'inherit',
      cwd: '../',
    });
  } catch (error) {
    console.error(error.stderr);
    process.exit(1);
  }

  process.exit(0);
};

updateAndInstall();

export default updateAndInstall;
