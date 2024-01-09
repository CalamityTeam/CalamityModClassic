using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point0.Items;

namespace CalamityModClassic1Point0.Items.Weapons {
public class Megalodon : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture = "CalamityModClassic1Point0/Items/Weapons/Megalodon");
		return true;
	}
	
    public override void SetDefaults()
    {
        //DisplayName.SetDefault("Megalodon");
        Item.damage = 61;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 72;
        Item.height = 32;
        //AddTooltip("A Megashark on Angry Vitamins");
        //AddTooltip2("50% chance to not consume ammo");
        Item.useTime = 3;
        Item.useAnimation = 3;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 3;
        Item.value = 750000;
        Item.rare = 10;
        Item.UseSound = SoundID.Item11;
        Item.autoReuse = true;
        Item.shoot = 10; //idk why but all the guns in the vanilla source have this
        Item.shootSpeed = 16f;
            Item.useAmmo = AmmoID.Bullet;
    }
    
    public override bool CanConsumeAmmo(Item ammo, Player player)
    {
    	if (Main.rand.Next(0, 101) <= 50)
    		return false;
    	return true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "BarofLife", 5);
        recipe.AddIngredient(ItemID.Megashark, 1);
        recipe.AddIngredient(null, "CoreofCalamity");
        recipe.AddIngredient(ItemID.ChainGun, 1);
        recipe.AddTile(null, "ParticleAccelerator");
        recipe.Register();
    }
}}