using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class ManaRose : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/ManaRose");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Mana Rose");
        Item.damage = 15;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 8;
        Item.width = 46;
        Item.height = 46;
        Item.useTime = 27;
        Item.useAnimation = 27;
        Item.useStyle = 5;
        Item.staff[Item.type] = true;
        ////Tooltip.SetDefault("Casts a mana bolt that explodes into smaller bolts");
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 3.25f;
        Item.value = 50000;
        Item.rare = 2;
        Item.UseSound = SoundID.Item109;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("ManaBolt").Type;
        Item.shootSpeed = 10f;
    }
    
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.NaturesGift);
        recipe.AddIngredient(ItemID.JungleRose);
        recipe.AddIngredient(ItemID.Moonglow, 5);
        recipe.AddTile(TileID.Anvils);
        recipe.Register();
    }
}}