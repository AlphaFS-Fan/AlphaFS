/*  Copyright (C) 2008-2015 Peter Palotas, Jeffrey Jangli, Alexandr Normuradov
 *  
 *  Permission is hereby granted, free of charge, to any person obtaining a copy 
 *  of this software and associated documentation files (the "Software"), to deal 
 *  in the Software without restriction, including without limitation the rights 
 *  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell 
 *  copies of the Software, and to permit persons to whom the Software is 
 *  furnished to do so, subject to the following conditions:
 *  
 *  The above copyright notice and this permission notice shall be included in 
 *  all copies or substantial portions of the Software.
 *  
 *  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 *  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 *  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 *  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 *  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
 *  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN 
 *  THE SOFTWARE. 
 */

using System.Collections.Generic;
using System.Security;
using SearchOption = System.IO.SearchOption;

namespace Alphaleonis.Win32.Filesystem
{
   partial class Directory
   {
      #region .NET

      /// <summary>Returns an enumerable collection of file names in a specified <paramref name="path"/>.</summary>
      /// <param name="path">The directory to search.</param>
      /// <returns>An enumerable collection of the full names (including paths) for the files in the directory specified by <paramref name="path"/>.</returns>
      [SecurityCritical]
      public static IEnumerable<string> EnumerateFiles(string path)
      {
         return EnumerateFileSystemEntryInfosCore<string>(null, path, Path.WildcardStarMatchAll, DirectoryEnumerationOptions.Files, PathFormat.RelativePath);
      }

      /// <summary>Returns an enumerable collection of file names in a specified <paramref name="path"/>.</summary>
      /// <returns>An enumerable collection of the full names (including paths) for the files in the directory specified by <paramref name="path"/> and that match the <paramref name="searchPattern"/>.</returns>
      /// <param name="path">The directory to search.</param>
      /// <param name="searchPattern">
      ///   The search string to match against the names of directories in <paramref name="path"/>.
      ///   This parameter can contain a combination of valid literal path and wildcard
      ///   (<see cref="Path.WildcardStarMatchAll"/> and <see cref="Path.WildcardQuestion"/>) characters, but does not support regular expressions.
      /// </param>
      [SecurityCritical]
      public static IEnumerable<string> EnumerateFiles(string path, string searchPattern)
      {
         return EnumerateFileSystemEntryInfosCore<string>(null, path, searchPattern, DirectoryEnumerationOptions.Files, PathFormat.RelativePath);
      }

      /// <summary>Returns an enumerable collection of file names that match a <paramref name="searchPattern"/> in a specified <paramref name="path"/>, and optionally searches subdirectories.</summary>
      /// <returns>An enumerable collection of the full names (including paths) for the files in the directory specified by <paramref name="path"/> and that match the specified <paramref name="searchPattern"/> and <paramref name="searchOption"/>.</returns>
      /// <param name="path">The directory to search.</param>
      /// <param name="searchPattern">
      ///   The search string to match against the names of directories in <paramref name="path"/>.
      ///   This parameter can contain a combination of valid literal path and wildcard
      ///   (<see cref="Path.WildcardStarMatchAll"/> and <see cref="Path.WildcardQuestion"/>) characters, but does not support regular expressions.
      /// </param>
      /// <param name="searchOption">
      ///   One of the <see cref="SearchOption"/> enumeration values that specifies whether the <paramref name="searchOption"/>
      ///   should include only the current directory or should include all subdirectories.
      /// </param>
      [SecurityCritical]
      public static IEnumerable<string> EnumerateFiles(string path, string searchPattern, SearchOption searchOption)
      {
         var options = DirectoryEnumerationOptions.Files | ((searchOption == SearchOption.AllDirectories) ? DirectoryEnumerationOptions.Recursive : 0);

         return EnumerateFileSystemEntryInfosCore<string>(null, path, searchPattern, options, PathFormat.RelativePath);
      }

      #endregion // .NET

      #region Transactional
      
      /// <summary>[AlphaFS] Returns an enumerable collection of file names in a specified <paramref name="path"/>.</summary>
      /// <returns>An enumerable collection of the full names (including paths) for the files in the directory specified by <paramref name="path"/>.</returns>
      /// <param name="transaction">The transaction.</param>
      /// <param name="path">The directory to search.</param>
      [SecurityCritical]
      public static IEnumerable<string> EnumerateFilesTransacted(KernelTransaction transaction, string path)
      {
         return EnumerateFileSystemEntryInfosCore<string>(transaction, path, Path.WildcardStarMatchAll, DirectoryEnumerationOptions.Files, PathFormat.RelativePath);
      }

      /// <summary>[AlphaFS] Returns an enumerable collection of file instances that match a <paramref name="searchPattern"/> in a specified <paramref name="path"/>.</summary>
      /// <returns>An enumerable collection of the full names (including paths) for the files in the directory specified by <paramref name="path"/> and that match the <paramref name="searchPattern"/>.</returns>
      /// <param name="transaction">The transaction.</param>
      /// <param name="path">The directory to search.</param>
      /// <param name="searchPattern">
      ///   The search string to match against the names of directories in <paramref name="path"/>.
      ///   This parameter can contain a combination of valid literal path and wildcard
      ///   (<see cref="Path.WildcardStarMatchAll"/> and <see cref="Path.WildcardQuestion"/>) characters, but does not support regular expressions.
      /// </param>
      [SecurityCritical]
      public static IEnumerable<string> EnumerateFilesTransacted(KernelTransaction transaction, string path, string searchPattern)
      {
         return EnumerateFileSystemEntryInfosCore<string>(transaction, path, searchPattern, DirectoryEnumerationOptions.Files, PathFormat.RelativePath);
      }

      /// <summary>[AlphaFS] Returns an enumerable collection of file instances instances that match a <paramref name="searchPattern"/> in a specified <paramref name="path"/>, and optionally searches subdirectories.</summary>
      /// <returns>An enumerable collection of the full names (including paths) for the files in the directory specified by <paramref name="path"/> and that match the specified <paramref name="searchPattern"/> and <paramref name="searchOption"/>.</returns>
      /// <param name="transaction">The transaction.</param>
      /// <param name="path">The directory to search.</param>
      /// <param name="searchPattern">
      ///   The search string to match against the names of directories in <paramref name="path"/>.
      ///   This parameter can contain a combination of valid literal path and wildcard
      ///   (<see cref="Path.WildcardStarMatchAll"/> and <see cref="Path.WildcardQuestion"/>) characters, but does not support regular expressions.
      /// </param>
      /// <param name="searchOption">
      ///   One of the <see cref="SearchOption"/> enumeration values that specifies whether the <paramref name="searchOption"/>
      ///   should include only the current directory or should include all subdirectories.
      /// </param>
      [SecurityCritical]
      public static IEnumerable<string> EnumerateFilesTransacted(KernelTransaction transaction, string path, string searchPattern, SearchOption searchOption)
      {
         var options = DirectoryEnumerationOptions.Files | ((searchOption == SearchOption.AllDirectories) ? DirectoryEnumerationOptions.Recursive : 0);

         return EnumerateFileSystemEntryInfosCore<string>(transaction, path, searchPattern, options, PathFormat.RelativePath);
      }

      #endregion // Transactional
   }
}
