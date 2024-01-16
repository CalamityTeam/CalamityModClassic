using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items;

namespace CalamityModClassic1Point1.Items.Weapons {
public class Megalodon : ModItem
{
	public override bool IsLoadingEnabled(Mod mod)/* tModPorter Suggestion: If you return false for the purposes of manual loading, use the [Autoload(false)] attribute on your class instead */
	{
		//texture =("CalamityModClassic1Point1/Items/Weapons/Megalodon");
		return true;
	}
	
    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Megalodon");
        Item.damage = 28;
        Item.DamageType = DamageClass.Ranged;
        Item.width = 72;
        Item.height = 32;
        ////Tooltip.SetDefault("A Megashark on Angry Vitamins\n50% chance to not consume ammo");
        Item.useTime = 6;
        Item.useAnimation = 6;
        Item.useStyle = 5;
        Item.noMelee = true; //so the item's animation doesn't do damage
        Item.knockBack = 2.5f;
        Item.value = 750000;
        Item.rare = 8;
        Item.UseSound = SoundID.Item11;
        Item.autoReuse = true;
        Item.shoot = 10; //idk why but all the guns in the vanilla source have this
        Item.shootSpeed = 16f;
        Item.useAmmo = 97;
    }
    
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
	    float SpeedX = velocity.X + (float) Main.rand.Next(-20, 21) * 0.05f;
	    float SpeedY = velocity.Y + (float) Main.rand.Next(-20, 21) * 0.05f;
	    Projectile.NewProjectile(source, position, new Vector2(SpeedX, SpeedY), type, damage, knockback, player.whoAmI, 0.0f, 0.0f);
	    return false;
	}
    
    public override bool CanConsumeAmmo(Item ammo, Player player)
    {
    	if (Main.rand.Next(0, 100) <= 50)
    		return false;
    	return true;
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(ItemID.Megashark);
        recipe.AddIngredient(null, "CoreofEleum", 7);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}