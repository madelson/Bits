# Bits

Bits is a tiny .NET library that provides implementations of various bitwise operations on all .NET primitive integral types.

Bits is available for download as a [NuGet package](https://www.nuget.org/packages/Bits). [![NuGet Status](http://img.shields.io/nuget/v/DistributedLock.svg?style=flat)](https://www.nuget.org/packages/Bits/)

[Release notes](#release-notes)

## Features

The various operations are contained in the `Bits` static class. All operations have overloads for `byte`, `sbyte`, `ushort`, `short`, `uint`, `int`, `ulong`, and `long` unless the specific operator does not make sense for a given type (e. g. `ReverseBytes` is not meaningful for single-byte types):

### Basics

Method | Notes
-------|------
HasAnyFlag | whether a value has *any* of the same bits set as another value
HasAllFlag | whether a value has *all* of the same bits set as another value
Get/Set/Clear/FlipBit | manipulates an individual bit in a value by index
ToShortBinaryString | binary representation of a value *without* leading zeros
ToLongBinaryString | binary representation of a value *with* leading zeros

### Type-preserving primitive operations

While primitive bitwise operations like `|` and `&` are defined natively for all primitive integral types, for 8- and 16- bit values these operators return `int` rather than the input type. This means that verbose casting is required and, in the case of the shift operators (`<<` and `>>`) leads to inconsistencies in how shifts by negative values are handled as compared to the larger types.

To simplify working with the smaller integral values, the following methods are provided for `sbyte`, `byte`, `ushort`, and `short`: `ShiftLeft`, `ShiftRight`, `And`, `Or`, `Xor`, and `Not`.

## Release notes
- 1.0.0 Initial release