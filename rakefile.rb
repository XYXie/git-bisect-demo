require 'rake/clean'
Dir.glob(File.join(File.dirname(__FILE__), 'tools/Rake/*.rb')).each do |f|
	require f
end

task :default => [:clobber, 'tests:run']

CLEAN.clear
CLEAN.include('teamcity-info.xml')
CLEAN.include('source/**/obj')
CLEAN.include('build/test-results/*')
		
CLOBBER.clear
CLOBBER.include('build')
CLOBBER.include('source/**/bin')

namespace :compile do
	desc 'Compiles the application'
	task :app => [:clobber] do
		MSBuild.compile :project => "source/Calculator/Calculator.csproj"
	end
end

namespace :tests do
	desc 'Runs unit tests'
	task :run => ['compile:app'] do
		Mspec.run \
			:tool => "tools/Machine.Specifications/mspec.exe",
			:reportdirectory => "build/test-results",
			:assembly => "build/tests/Calculator.dll"
	end
end