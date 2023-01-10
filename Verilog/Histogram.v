//[$SpectrumWidth 7]
//[InferClocks]
module Histogram(
    //[Clock 100 MHz]
    input  my_100MHz_clk,

    //[Reset]
    //[InitialValue 1]
    //[ResetTime 1000]
    input  reset,

    input storeData,
    input startReadout,
    input [spectrumWidth - 1:0] dataValue,
    output readingOut,
    output [spectrumWidth - 1:0] histogramValue
);


//[Set $SpectrumWidth]
parameter spectrumWidth = 7;
parameter spectrumMax = 1 << spectrumWidth - 1;

//[RisingEdge]
wire risingThisEdge;

//[RisingEdge]
wire risingStoreData;
//[Previous]
reg p1_risingStoreData;
//[Previous]
reg p2_risingStoreData;

//[RisingEdge]
wire risingStartReadout;

assign readingOut = memoryState == readingOutState;
assign histogramValue = readingOut ? histogramDataOutA : 0;

//[StateMachine memoryState
//storingState : risingStartReadout => readingOutState,
//               risingStop => resetState
//readingOutState : histogramAddressA == spectrumMax => storingState]


//[$Memory histogram]
//[DualMemory --name $Memory --dataWidth 16 --addressWidth 7 --previousAddressA]

//[MemoryBlock $Memory A]
begin
    //[Reset]
        //[<= 0]
    else begin
        case(memoryState)
            storingState : begin
                if(risingStartReadout)
                    //[<= 0]
                else if(risingStoreData)
                    //[<= dataValue]
                else
                    //[<= 0]
            end
            readingOutState : begin
                //[<= histogramAddressA + 1]
            end
        endcase
    end
end

//[MemoryBlock $Memory B]
begin
    if(reset)
        //[<= 0]
    else begin
        case(memoryState)
            storingState : begin
                if(p2_risingStoreData)
                    //[<= p1_histogramAddressA => histogramDataOutA + 1]
                else
                    //[<= 0]
            end
            readingOutState : begin
                //[<= 0]
            end
        endcase
    end
end
endmodule