/*
Copyright (c) 2022, Icii Technologies LLC
All rights reserved.

This source code is licensed under the BSD-style license found in the
LICENSE file in the root directory of this source tree. 
*/

module HistogramTest;


//[TestModule Histogram]


task StoreValue;
    input [spectrumWidth - 1:0] valueToSend;

    begin
        inputValue = valueToSend; #20; store = 1; #20; store = 0; #50;
    end

endtask


initial begin

    #2002;

    //[StoreValues 5000]

    startReadout = 1; #20;
    startReadout = 0;

    #10000;
    $finish;
end

endmodule
