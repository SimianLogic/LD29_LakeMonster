#!/usr/bin/env ruby

require 'FileUtils'

def write_pretty_metadata(metadata)

  new_file_name_pieces = metadata.split('/')
  new_file_name_pieces.pop
  new_file_name = new_file_name_pieces.join('/') + "/pretty_metadata.txt"

  new_file = File.open(new_file_name, "w")
  new_file.puts"*"*10 + " #{ metadata.split('/')[1]} " + "*"*10
  new_file.puts #empty line

  buttons = []
  images = []
  labels = []
  
  begin
    file = File.open(metadata,"r")
  rescue
    p "UNABLE TO OPEN #{metadata}"
    return
  end
  data = file.read.split("+")

  data.each do |item|
      item_data = item.split("|")
      if item_data.first == "root_width"
        #do nothing
      elsif item_data == "root_height"
    #do nothing
      elsif item_data.first[0..3] == "text"
        labels << item
      elsif item_data.first[0..2] == "btn" and item_data.first[-2..-1] == "up"
        buttons << item
      else
        images << item
      end
  end

  indent = 5
  new_file.puts"*"*indent + "IMAGES"
  images.each do |item|
  image_data = item.split("|")
    new_file.puts" "*indent*2 + image_data[0] + "       (#{image_data[1]},#{image_data[2]})"
  end
  new_file.puts

  new_file.puts"*"*indent + "BUTTONS"
  buttons.each do |item|
  button_data = item.split("|")
    new_file.puts" "*indent*2 + button_data.first[4..-4] + "       (#{button_data[1]},#{button_data[2]})"
  end
  new_file.puts

  new_file.puts"*"*indent + "TEXT FIELDS"
  labels.each do |item|
    label_data = item.split("|")
    new_file.puts" "*indent*2 + label_data[0][5..-1] + "       (#{label_data[1]},#{label_data[2]})"
    new_file.puts" "*indent*4 + "        COLOR: #{label_data[3]}"
    new_file.puts" "*indent*4 + "         FONT: #{label_data[4]}"
    new_file.puts" "*indent*4 + "JUSTIFICATION: #{label_data[5]}"
    new_file.puts" "*indent*4 + "    FONT SIZE: #{label_data[6]}"
    new_file.puts" "*indent*4 + " DEFAULT TEXT: #{label_data[9]}"
  end

end

def copy_and_move(folder, root_name)
  metadata    = "#{folder}/#{root_name}/metadata.txt"
  new_metadata = "#{folder}/#{root_name}/#{root_name}.txt"
  
  return if !File.exists?(metadata)
  
  write_pretty_metadata(metadata)

  FileUtils.cp(metadata, new_metadata)
  FileUtils.mv(new_metadata, "lake_monster_unity/Assets/Resources/Metadata")
end

metadata = Dir.glob("documents/**/metadata.txt")
metadata.each do |meta|
  root_name = meta.split("/")[1]
  copy_and_move("documents", root_name)
end

#documents was our prototype UI folder... finished stuff is going into UI
#it is very possible (and likely) that things in UI will overwrite metadata from documents
metadata = Dir.glob("UI/**/metadata.txt")
metadata.each do |meta|
  root_name = meta.split("/")[1]
  copy_and_move("UI", root_name)
end

animations = Dir.glob("assets/**/*.json")
animations.each do |anim|
  file_name = anim.split("/").last
  if(file_name != "library.json")
    FileUtils.cp(anim, "lake_monster_unity/Assets/Resources/Animation")
    FileUtils.cp(anim, "lake_monster_unity/Assets/Resources/Animation")    
  end
end
