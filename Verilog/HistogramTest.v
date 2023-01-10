//[!DontProcess]
module HistogramTest;



//[TestModule Histogram]

//Note: User should not have to declare these here, will be improved with update to TestModule
//*****************************
/*[Clock]*/
wire my_100MHz_clk;
/*[Reset]*/
wire reset;
//*****************************

task StoreValue;
input [spectrumWidth - 1:0] valueToSend;

    begin
        inputValue = valueToSend; #20; store = 1; #20; store = 0; #50;
    end

endtask

reg myReg;
//[Previous]
reg p1_myReg;


initial begin

    #2002;

    //[StoreValues 50]

    startReadout = 1; #20;
    startReadout = 0;

    #10000;
    $finish;
end
endmodule