XProc
=====

XProc is a console application for batch processing XSL transforms.

Usage
-----

XProc.exe INPUT_DIR OUTPUT_DIR STYLESHEET

* INPUT_DIR is the input directory containing xml files to be transformed.
* OUTPUT_DIR is the output directory. It will be created if it does not already exist.
* STYLESHEET is an XSL stylesheet used to transform the files in INPUT_DIR.

The basic process is to apply the stylesheet to each xml file in INPUT_DIR and place the result in OUTPUT_DIR. Processed files will have the same name as the original input.

Limitations
-----------

Reusing the original file's name in the output directory means we are not performing a very common use case - that of changing the file extension. The current behaviour is to specification, but this feature would make the tool more useful.

The command line argument parsing is very primitive. A better interface could be achieved with a little more time.

There is no way to change the type of the Logger. An improved API would facilitate this, and would be relatively easy to implement, as the existing Logger can only be acquired from a factory.

Design Notes
------------

The Logger is probably unnecessary but has been useful in development. By default it defaults to Warning level in the release build so it does no strictly need to be removed.

The batcher processes files using the simplest .Net 4 concurrency mechanism available - `Parallel.ForEach`. This will allow the framework to select the most appropriate partitioning scheme and run the batch process across multiple threads. This behaviour can be invoked synchronously via `ProcessBatch` or asynchronously  via `ProcessBatchAsync`. The asynchronous methods notify the client either via an event or through a client-suplied callback method. The asynchronous methods are not to be trusted as I haven't had time to properly test them yet.


Core API Usage
--------------

The XSL transformer can be selected by implementing IXslTransformer and providing it to the constructor of Batcher. This makes it easy to use a different XSLT API from the .Net platform default and add any additional features, like changing the extension of the output file.

To process the batch job, call `Batcher.ProcessBatch` or `Batcher.ProcessBatchAsync`. The XML documentation covers how to use the parameters.

The logger can be used by calling `LoggerService.GetLogger`.
