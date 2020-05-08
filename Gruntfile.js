module.exports = function (grunt) {
  // load the package JSON file
  var pkg = grunt.file.readJSON('package.json');

  // paths
  var sourceDir = 'src/';
  var tmpDir = 'tmp/';
  var releaseDir = 'release/';
  var projectDir = sourceDir + pkg.name + '/';
  var releaseFilesDir = releaseDir + 'files/';
  var tmpDirRelease = tmpDir + releaseFilesDir;

  // load information about the assembly
  var assembly = grunt.file.readJSON(projectDir + 'Properties/AssemblyInfo.json');

  // get the version of the package
  var version = assembly.informationalVersion ? assembly.informationalVersion : assembly.version;

  var nuspecFileName = pkg.name + '.nuspec';

  var buildedNuget = {};

  buildedNuget[`${tmpDir}${sourceDir}${pkg.name}/${nuspecFileName}`] = [projectDir + nuspecFileName]

  grunt.template.addDelimiters('handlebars-like-delimiters', '{{', '}}');
  grunt.initConfig({
    pkg: pkg,
    clean: {
      release: ['release'],
      tmp: ['tmp'],
    },
    copy: {
      bacon: {
        files: [
          {
            expand: true,
            cwd: projectDir + 'obj/Release/',
            src: [pkg.name + '.dll', pkg.name + '.xml'],
            dest: releaseFilesDir + 'bin/',
          },
          {
            expand: true,
            cwd: projectDir + 'Web/UI/',
            src: ['**'],
            dest: releaseFilesDir,
          },
        ],
      },
      nuget: {
          files: [
              {
                  src: [ sourceDir + '**/*.*'],
                  dest: tmpDir
              }
          ]
      }
    },
    template: {
      nuspec: {
        options: {
          data: {
            id: pkg.name,
            name: pkg.name,
            title: pkg.name,
            version: pkg.version,
            description: pkg.readme,
            author: pkg.author.name,
            copyright: assembly.copyright,
            files: [
              {
                path: tmpDir,
                target: sourceDir + '**/*.*',
              },
            ],
          },
          delimiters: 'handlebars-like-delimiters',
        },
        files: [
          buildedNuget
        ],
      },
    },
    zip: {
      release: {
        cwd: releaseFilesDir,
        src: [releaseFilesDir + '**/*.*'],
        dest: releaseDir + 'zip/' + pkg.name + '.v' + version + '.zip',
      },
    },
    umbracoPackage: {
      dist: {
        src: releaseFilesDir,
        dest: releaseDir + 'umbraco/',
        options: {
          name: pkg.name,
          version: version,
          url: pkg.url,
          license: pkg.license.name,
          licenseUrl: pkg.license.url,
          author: pkg.author.name,
          authorUrl: pkg.author.url,
          readme: pkg.readme,
          outputName: pkg.name + '.v' + version + '.zip',
        },
      },
    },
    nugetpack: {
      dist: {
        src: tmpDir + sourceDir + pkg.name + '/' + pkg.name + '.csproj',
        dest: releaseDir + 'nuget/',
        options: {
          properties: 'Platform=AnyCPU;Configuration=Release',
        },
      },
    },
  });

  grunt.loadNpmTasks('grunt-umbraco-package');
  grunt.loadNpmTasks('grunt-contrib-clean');
  grunt.loadNpmTasks('grunt-contrib-copy');
  grunt.loadNpmTasks('grunt-nuget');
  grunt.loadNpmTasks('grunt-zip');
  grunt.loadNpmTasks('grunt-template');

    grunt.registerTask('dev', ['clean', 'copy', 'template', 'zip', 'umbracoPackage', 'nugetpack']);

  grunt.registerTask('default', ['dev']);
};
