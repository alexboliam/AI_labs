% Copyright

implement main
    open core, console, list, string, depth

class facts
    volums : (positive*) determ.

clauses
    volums([5, 7]).

class predicates
    jmove : depth::move{positive*}.
clauses
    jmove(L, Str) = L1 :-
        memberIndex_nd(A, I, L),
        A > 0,
        setNth(I, L, 0, L1),
        Str = format("Вылить % л из %-го сосуда", A, I + 1).
    jmove(L, Str) = L2 :-
        volums(VL),
        memberIndex_nd(A, I, L),
        A > 0,
        memberIndex_nd(B, J, L),
        J <> I,
        V = nth(J, VL),
        B < V,
        Vol = math::min(V - B, A),
        setNth(I, L, A - Vol, L1),
        setNth(J, L1, B + Vol, L2),
        Str = format("Перелить % л из %-го сосуда в %-й", Vol, I + 1, J + 1).
    jmove(L, Str) = L1 :-
        volums(VL),
        memberIndex_nd(A, I, L),
        V = nth(I, VL),
        A < V,
        setNth(I, L, V, L1),
        Str = format("Налить % л в %-й сосуд", V - A, I + 1).

    run() :-
        Start = [0, 0],
        Goal = [0, 6],
        tuple([S0 | P], Moves) = depthSearch(jmove, Start, Goal, _),
        V = varM::new(1),
        write("\t", S0),
        nl,
        forAll(zip(P, Moves),
            { (tuple(X, S)) :-
                writef("%. %\n\t%\n", V:value, S, X),
                V:value := V:value + 1
            }),
        nl,
        fail
        or
        _ = readLine().

end implement main

goal
    console::runUtf8(main::run).