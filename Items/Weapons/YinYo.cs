using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class YinYo : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/YinYo");
		return true;
	}
	
    public override void SetDefaults()
    {
    	Item.CloneDefaults(ItemID.Chik);
        //Tooltip.SetDefault("YinYo");
        Item.damage = 40;
        Item.useTime = 25;
        ////Tooltip.SetDefault("Fires light or dark shards when enemies are near\nLight shards fly up and down while dark shards fly left and right");
        Item.useAnimation = 25;
        Item.useStyle = 5;
        Item.channel = true;
        Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        Item.knockBack = 3.2f;
        Item.value = 130000;
        Item.rare = 5;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("YinYo").Type;
    }
    
    public override void AddRecipes()
	{
		Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.DarkShard);
        recipe.AddIngredient(ItemID.LightShard);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
	}
}}